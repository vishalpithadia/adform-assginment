# Assignment 
As per the pdf provided the application is using denmark national bank api to fetch the currency rates.following are the api available
 1. API to query all current rates at 100 units of foreing currency
 2. Conversion of single Foregin currency to DKK
 3. History of already calculated Currency rates

# How to run the Project

> **Important:** You must have **Docker Desktop** or **Docker Compose** installed on your system to run this project.

Check out the files from the git and run following command 

```
docker-compose up
```

**if you run this command first time it might take 10 to 15 mins as all the images will be downloaded please have patience**

then Navigate to followin Url

```
http://localhost:8080/swagger/
```

**<span style="color:red;">Note:</span>** if it is still does not work rerun the following command
```
docker-compose up
```

**<span style="color:red;">Note:</span>** Make sure Docker Desktop is running before executing the above commands. 


to shutdown the project run following command 

```
docker-compose down
```

**<span style="color:red;">Note:</span>** After shuttingdown the docker data will not persist as volume is not being utilised here
