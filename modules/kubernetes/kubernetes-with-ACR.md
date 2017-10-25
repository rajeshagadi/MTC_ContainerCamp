## Deploy Containers to Azure ACS with Kubernetes

## Task 1: Azure CLI 2.0 & Login To Azure
1. Install the lastest version of the Azure CLI.  Go to the [Install Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli?view=azure-cli-latest) page and follow the instructions for your host OS.  You can also use the [Azure Portal Cloud Shell](https://docs.microsoft.com/en-us/azure/cloud-shell/overview?view=azure-cli-latest) if you do not want to install the Azure CLI locally.  
  Another option is to use a Docker container with the Azure CLI pre-installed.  Run the following command inside your shell (i.e., command prompt, Powershell, Bash, etc.):
      ```none
      docker run -it -p 8001:8001 azuresdk/azure-cli-python:latest bash 
      ```
2. If you have not done so already, open a shell environment
3. Execute the following command:
    ```none
    az login -u <username> -p <password>
    ```
    A successful login will result in JSON output similar to the following:
    ```json
    [
       {
        "cloudName": "AzureCloud",
        "id": "6e7ce629-5859-4837-bce5-571fe7b268c5",
        "isDefault": false,
        "name": "MTC Houston Labs",
        "state": "Enabled",
        "tenantId": "a8e59e50-6360-4372-a629-9a9bf465158e",
        "user": {
          "name": "ratella@mtchouston.net",
          "type": "user"
        }
      }
    ]
    
    ```
    > Note:  If your AAD policy requires multi-factor authentication, you will need to excute the following command:
    > ```none
    > az login
    > ```
    > You will receive a token that you must then authenticate your device with at [http://aka.ms/devicelogin](http://aka.ms/devicelogin).  It is recommended to access that Url with a InPrivate/InCognito browser session to avoid cookie conflicts.
4. This lab assumes you will be using the default subscription associated with your Azure login Id.  If you want to change subscriptions, find the **"id"** value from the appropriate subscription in the returned json and execute the following command:
    ```none
    az account set --subscription="<SUBSCRIPTION_ID>"
    ```

## Task 2: Create ACS Cluster
1. Create a new resource group for your ACS to reside in:
    ```none
    az group create --name=<RESOURCE_GROUP_NAME> --location="<AZURE_REGION>"
    ```
    **Example**
        ```none
        az group create --name=ACSWorkshop --location="SouthCentralUS"
        ```
2.  Create your ACS cluster using Kubernetes with the following command:
    ```none
    az acs create --orchestrator-type=kubernetes --resource-group=<RESOURCE_GROUP_NAME> --name=<CLUSTER_NAME> --dns-prefix=<ANYVALUE> --generate-ssh-keys
    ```
    **Example**
    ```none
    az acs create --orchestrator-type=kubernetes --resource-group=ACSWorkshop --name=acskubernetes --dns-prefix=acstest --generate-ssh-keys
    ```

## Task 3: Create an Azure Container Registry
Azure Container Registry (ACR) is an Azure-based, private registry, for Docker container images. 
1.  Create a new Azure Container Registry with the following command:
     ```none
    az acr create --resource-group <RESOURCE_GROUP_NAME> --name <ACR_NAME> --sku Basic --admin-enabled true
    ```
    **Example**
    ```none
    az acr create --resource-group ACSWorkshop --name myacr --sku Basic --admin-enabled true
    ```
2. To push images to the registry, we must authenticate with the registry.  Use the following command to authenticate with ACR"
 ```none
    az acr login --name <ARC_NAME>
    ```
    **Example**
    ```none
    az acr login --name myacr
    ```


## Task 3: Install kubectl
Kubectl is the command line tool for administering your ACS Kubernetes cluster.

1. Install the *kubectl* tool with the following command:

    ```none
    az acs kubernetes install-cli
    ```
2. Validate that *kubectl* has been successfully installed by running:
    ```none
    kubectl version
    ```

## Task 4: Connect to the Cluster with *kubectl*
1. Run the following commadn to download the client credentials needed to access the Kubernetes cluster:

    ```none
    az acs kubernetes get-credentials --resource-group=<RESOURCE_GROUP_NAME> --name=<CLUSTER_NAME>
    ```
    **Example**
    ```none
    az acs kubernetes get-credentials --resource-group=ACSWorkshop --name=acskubernetes
    ```
## Task 5: Deploy the application to Kubernetes
In this task, you will deploy the readinglist application stack to Kubernetes cluster. In kubernetes a group of one or more containers run as a pod. Pods can also have shared storage for the containers running in the pod. 

At the end of this task you will have a total of 3 pods. Two for the app tier and one for MySQL.. The app tier pods will have both �ReadingList web app� and �Recommendation service�. There will be a total of 5 containers across 3 pods. 

1. If you do not already have Git installed on your machine, [follow the instructions for installing Git](https://git-scm.com/book/en/v2/Getting-Started-Installing-Git)
2. Clone the repository containing this tutorial with the following command:
    ```none
    git clone https://github.com/Microsoft/MTC_ContainerCamp
    ```
3. Make the directory containing the deployment *.yaml* files your current directory - */MTC_ContainerCamp/modules/kubernetes/src/deploy*
4. Deploy a Kubernetes pod containing a mysql database using the following command:
    ```none
    kubectl create -f mysql-deployment.yaml
    ```
5. Deploy the web UX and web service of the application using the following command:
    ```none
    kubectl create -f readinglist-deployment.yaml
    ```
6. Expose the web UX as a public service by running the following command:
    ```none
    kubectl create -f ./readinglist-service.yaml
    ```
7. Monitor the service creation and find the public IP address when the load balancer is configured by running the following command:
    ```none
    kubectl get svc web
    ```
    You will have to execute the command several times while you wait for the load balancer to be properly configured.  The IP address will change from *pending* to a valid IP address when finished.

8.  Access the Reading List application by opening your browser to http://<PublicIP>/readinglist

9. You can check the number of pods running in your cluster with the following command:
    ```none
    kubectl get pods
    ```
10. You can scale the number of pods used by your application at any time. To scale the number of pods for the web tier of the Reading List application, run the following command:
    ```none
    kubectl scale deployment/web --replicas=3
    ```
11. Check the number of pods now being used by the application by running this command:
    ```none
    kubectl get pods
    ```
12. Determine the number of web tier pod endpoints being load balanced by the app service tier with the following command:
    ```none
    kubectl get ep web
    ```

## Task 6: Explore the Kubernetes cluster with the Dashboard
The Kubernetes Dashboard is web interface that provides general-purpose monitoring and operations for Kubernetes clusters.  You can access this dashboard from your local machine via a proxy tunnel created by the *kubectl* tool.

1. To open a proxy tunnel to the Kubernetes Dashboard, run the following command:
    ```none
    kubectl proxy -p 8001
    ```
2. Open the browser on you machine and navigate to [http://localhost:8001:/ui](http://localhost:8001:/ui)

