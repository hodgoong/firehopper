# firehopper

[![Join the chat at https://gitter.im/firehopper4grasshopper/Lobby](https://badges.gitter.im/firehopper4grasshopper/Lobby.svg)](https://gitter.im/firehopper4grasshopper/Lobby?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)
![firehopper_logo](https://github.com/hodgoong/firehopper/blob/master/firehopper_logo.jpg)
___Rhino Grasshopper's RESTful interface for Google Firebase___

Firehopper is a Grasshopper add-in that interacts with Google Firebase in RESTful ways. User can simply request a PUT to store JSON style data in Google Firebase, PATCH to modify, POST to incrementally save data, DELETE to delete, and reqeust GET to fetch the data. This component will be updated further to support different functionality supported by Google Firebase

#### Last Update:
4 APR 2018

#### Download
http://www.food4rhino.com/app/firehopper

## How to use
![firehopper_components](https://github.com/hodgoong/firehopper/blob/master/examples/firehopper_example_sequencial.PNG)

### Setup
#### Create Google Firebase account for free
Visit http://firebase.google.com to create an account and to create new project.

#### Retrieve DB URL and API Key
After creating the account and the project, you can get the following information from 'Project Overview' section in the Firebase console. We need 'apiKey' and 'databaseURL' values.
![firehopper_firebase_setup](https://github.com/hodgoong/firehopper/blob/master/examples/firehopper_example_firebaseSetup.PNG)

#### Download Firehopper
Download firehopper.gha in the 'Release' tab in this github repo or at food4rhino(http://www.food4rhino.com/app/firehopper) and save under the Grasshopper library folder.

### Data
#### firehopper Key-Value Pair Generator
![firehopper_key-value pair generator](https://github.com/hodgoong/firehopper/blob/master/src/firehopper/Resources/firehopper_icon_keyval.png)

The key-value pair generator combines two sets of strings into JSON style string format. The component will throw an error when two lists contain different amount of string elements inside. 
![firehopper_key-value pair generator example](https://github.com/hodgoong/firehopper/blob/master/examples/firehopper_example_keyvalgen.PNG)

### HTTP
#### firehopper PUT 
![firehopper_PUT_component](https://github.com/hodgoong/firehopper/blob/master/src/firehopper/Resources/firehopper_icon_put.png)

The PUT component triggers PUT request to Firebase to store the JSON string. This component requires 'apiKey' and 'databaseURL' information retrieved from the 'Setup' section in this page. Basic usage of the PUT component is illustrated on below images:
![firehopper_PUT example](https://github.com/hodgoong/firehopper/blob/master/examples/firehopper_example_put.PNG)

After triggering the PUT component, the component will send the JSON string to corresponding Firebase database using the API key and Database URL. The result of above example is shown below:
![firehopper_PUT example_result](https://github.com/hodgoong/firehopper/blob/master/examples/firehopper_example_firebasePut.PNG)

#### firehopper PUT with Key-Value Generator
The PUT component can be used with the firehopper Key-Value Generator as well.
![firehopper_PUT example](https://github.com/hodgoong/firehopper/blob/master/examples/firehopper_example_keyvalgen+put.PNG)

The result of above example is shown below:
![firehopper_PUT example_result](https://github.com/hodgoong/firehopper/blob/master/examples/firehopper_example_firebaseKeyValGen+put.PNG)

#### firehopper GET
![firehopper_GET_component](https://github.com/hodgoong/firehopper/blob/master/src/firehopper/Resources/firehopper_icon_get.png)

The GET component triggers GET request to Firebase to fetch the JSON string. This component requires 'apiKey' and 'databaseURL' information retrieved from the 'Setup' section in this page. Basic usage of the GET component is illustrated on below images:
![firehopper_PUT example](https://github.com/hodgoong/firehopper/blob/master/examples/firehopper_example_get.PNG)

#### Query using ghJSON
JSON string retrieved from firehopper GET component can be further queried with ghJSON(https://mathrioshka.ru/ghjson/) as below example:
![firehopper_GET_component](https://github.com/hodgoong/firehopper/blob/master/examples/firehopper_example_queryUsingGHJSON.PNG)

#### firehopper POST
![firehopper_GET_component](https://github.com/hodgoong/firehopper/blob/master/src/firehopper/Resources/firehopper_icon_post.png)

Will be updated soon.

#### firehopper PATCH
![firehopper_GET_component](https://github.com/hodgoong/firehopper/blob/master/src/firehopper/Resources/firehopper_icon_patch.png)

Will be updated soon.

#### firehopper DELETE
![firehopper_GET_component](https://github.com/hodgoong/firehopper/blob/master/src/firehopper/Resources/firehopper_icon_delete.png)

Will be updated soon.

#### Usage Example with Geometry Parameter
![firehopper_GET_component](https://github.com/hodgoong/firehopper/blob/master/examples/firehopper_example_sphere.PNG)

#### Development Environment:
- Microsoft Visual Studio Community 2017 Version 15.5.0
- Rhino Version 5 SR14 64-bit
- Grasshopper Build 0.9.0076

#### Dependancies (run-time version):
- Grasshopper.dll (v4.0.30319)
- System.Net.Http.dll (v4.0.30319)

## Author
Hojoong Chung - https://github.com/hodgoong http://hojoongchung.wordpress.com hodgoong@gmail.com

firehopper © 2018 Hojoong Chung
