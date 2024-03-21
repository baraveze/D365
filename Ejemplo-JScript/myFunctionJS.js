// This is an example of JScript function
function myFunction(executionContext) {

var formContext = executionContext.getFormContext();

// Get value from any text field
var fieldValue = formContext.getAttribute("theNameOfTheField").getValue();
alert(fieldValue);

// Set value
formContext.getAttribute("fieldValue").setValue("NewValue");

// Alert new value
fieldValue = formContext.getAttribute("theNameOfTheField").getValue();
alert(fieldValue);

}