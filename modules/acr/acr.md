## Deploy Containers to Azure Containers Registry with Continious Deployment

In this lab we will create an Azure Web app for container (on Linux) and setup an docker / custom image to be deployed from the new Azure Container Registry via automate the deployment Continious Deployment.

## Task 1: Pick your Docker image to test

1. Download docker CLI or docker for windows. I am installing docker on my local windows 10 from https://www.docker.com/docker-windows . After installation ensure that you can see the docker icon in tray.

 
2. Signup for a docker account at https://cloud.docker.com/ & activate the account & sign in .

3. Login to docker on your PC/Mac.

4. Lets use nginx image as sample image so pull the image. 

```	 
		docker pull nginx
```

5. Lets ensure image is up and running on command prompt:

```
		docker run -it --rm -p 8080:80 nginx
```		 
		Check in browser open url  http://localhost:8080/ 

6. Now Ctl+C to break off. 


## Task 2: Create new Azure Container Registry and push your docker image

7. Create an Azure Container Registry:
		Refer here : https://docs.microsoft.com/en-us/azure/container-registry/container-registry-get-started-portal 
		 
		     Login to azure portal and create a Azure Container Registry over there from market place & get the ID and password for Azure Container Registry  as per documentation link above.
		 
		
		Please enable the Admin User option .
		Please note that Login Server name  , which is  <registoryname>. azurecr.io
		 
		From here onward for example purpose we shall use the registoryname as myregistry considering you have created the registry using this name.
		 
		Also note the Username and password.
		 
		
		 
8. Login to Azure Container Registry from docker.
		        Run this command on CLI 
		docker login myregistry.azurecr.io -u xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx -p <password>
		 
		Substitue above user name for -u and password for -p
		Now my CLI / Local docker is connected with Azure Container Registry.
		
9. Map the Tagged container image with Azure Container Registry account with a repository name and tags.
		 
```		 
		docker tag nginx myregistry.azurecr.io/samples1/php
```
		In above command, your local image named nginx is being mapped with my azure container registry end point myregistry.azurecr.io/samples1/php  which is actually creating a repository image with name samples1/php. 
				 
		we will see this repository in Azure Portal, once we push the image in next step.
		 
		Please note : don’t get confused with tag here , the tag in step 5 is the tag for image and we can have multiple tags and default tag is “latest”.
		 
		But Tag command here is kind of mapper between your image and registry end point.
		 
10. Push the local docker image to Azure Container Registry.

```
		docker push myregistry.azurecr.io/samples1/php
```
## Task 3: Create new Web app for Containers

11. From the Azure Portal,  create a new Web app for containers,for Docker Container option, select the Azure Container registry.
		 
12. Browser the web app to see if the image is up and running on Web app. Now browsing my web app will show the container image.		 
		
## Task 4: Enable Continous Deployment

13. From you Web app for Containers, in the App settings, add an app setting called DOCKER_ENABLE_CI with the value true. 
		
		 
14. Get the service URL from publishing profile. For the Webhook URL, you need to have the following endpoint: https://<publishingusername>:<publishingpwd>@<sitename>.scm.azurewebsites.net/docker/hook. You can obtain your publishingusername and publishingpwd by downloading the web app publish profile using the Azure portal.

So service url looks like :
		https://<publishingusername>:<publishingpwd>@<sitename>.scm.azurewebsites.net/docker/hook.
	
15. Go to Azure Container Registry & create Web hook.
		Couple of values which you will require are :
		Webhookname : any meaning full name
		Service URI : the service uri from step number 14 something like below : 
		 
		https://<publishingusername>:<publishingpwd>@<sitename>.scm.azurewebsites.net/docker/hook
		 
		Scope : this will be in format of Image:Tag  	
	
16. Fill up the values and save and do a test ping.
		 
		Sometime your kudu is down or process not running ,thus you may encounter error , please make sure your KUDU process is running while ping or deployment .
		 
        Now your continuous deployment is up and running
        
        
        you can test it with deploying modified image or new image with same tag , lets see how to do it ?
## Task 5: Testing Continous Deployment
17. Modify the locally residing nginx image 
18.  rebuild it so that changes are persistence 
19. push the new image on Azure portal to verify it should auto deploy .
20. Browse the web app to see that new image is up and running.
