function validateForm() {
    if (Page_ClientValidate()) {
        return true;
    }
    else {
        return false;
    }
}