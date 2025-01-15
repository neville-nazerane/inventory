



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



window.openModal = openModal;
window.closeModal = closeModal;
window.closeModal = showError;