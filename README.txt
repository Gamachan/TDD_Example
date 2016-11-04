Solution Notes

Application was developed with TDD methodology and includes relevant Unit Tests for business logic components.

For the development purposes the following assumptions were made:
* In case of a single invalid item in the shopping basket application doesn't continue calculation of a receipt and throws exception with relevant details. Including:
	- Invalid amount value
	- Invalid item name value
	- Invalid price value
* Input and output data is loaded from relevant text files located in Resources folder. This logic is loosely coupled and can be substitute with data loading from any other resource, e.g. DB, API.... 
* For more sufficient user experience in case of multiple errors all of them are aggregated and displayed to user simultaneously.
* Catalogue files were created containing associating products in order to handle relevant categories. In addition exempted category list was created for distinct categories which are exempted. 
