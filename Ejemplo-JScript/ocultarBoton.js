/**
 * Este ejemplo es muy básico y sirve para ejemplificar la porción de código
 * que se debe colocar para ocultar un botón en Dynamics 365 CRM. A medida que
 * uno va adquiriendo mas experiencia y conocimientos, este ejemplo ya no va a servir
 **/

function ShowButton (PrimaryControl) {
    
    var primaryContact = PrimaryControl.getAttribute("primarycontactid");

    if (primaryContact !== null) {
        
        var primaryContactValue = primaryContact.getValue();

        if (primaryContactValue !== null && primaryContactValue !== undefined) {

            return false; // Si contacto tiene valor queremos esconder el boton, por eso devolvemos false.
        } else {

            return true;
        }

            
    } else {

        return true;
    }
}
