



var modals = [];

function getModal(id)
{
    var modal = modals[id];
    if (!modal)
        modals[id] = modal = new bootstrap.Modal(document.getElementById(id))
    return modal;
}

function openModal(id)
{
    getModal(id).show();
}

function closeModal(id)
{
    getModal(id).hide();
}

function showError(errorMsg)
{
    document.getElementById("errorMessage").innerHTML = errorMsg;
    openModal("errorModal");
}


function saveToLocalStorage(key, value) {
    localStorage.setItem(key, value);
}

function getFromLocalStorage(key) {
    return localStorage.getItem(key);
}

function deleteFromLocalStorage(key) {
    localStorage.removeItem(key);
}


window.openModal = openModal;
window.closeModal = closeModal;
window.showError = showError;
window.saveToLocalStorage = saveToLocalStorage;
window.getFromLocalStorage = getFromLocalStorage;
window.deleteFromLocalStorage = deleteFromLocalStorage;
