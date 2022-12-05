"using strict";


(function _CreateCampaignModal() {
    // If there are any error messages, clear them
    _clearErrorMessages();
    console.log("Create Campaign Modal Reached.");
    const createCampaignModalDOM = document.querySelector("#createCampaignModal");
    const createCampaignModal = new bootstrap.Modal(createCampaignModalDOM);
    const createQuoteButton = document.querySelector("#btnCreateCampaign");
    createQuoteButton.addEventListener("click", event => {
        createCampaignModal.show();
    })

    const createCampaignForm = document.querySelector("#createCampaignForm");
    createCampaignForm.addEventListener("submit", async (event) => {
        event.preventDefault();
        _clearErrorMessages();
        await _submitNewCampaign(createCampaignForm);

    });
    async function _submitNewCampaign(createCampaignForm) {
        const url = createCampaignForm.getAttribute('action');
        const formData = new FormData(createCampaignForm);
        const response = await fetch(url, {
            method: "post", body: formData
        });
        if (response.ok == false) {
            throw new Error("There was an HTTP error!");
        }
        const result = await response.json();
        if (result === "fail") {
            return;
        }
        console.log(result.quoteId);
        createCampaignModal.hide();
        await _updateCampaignCardDiv(result.campaignId);
    }

    //The problem child.
    async function _updateCampaignCardDiv(campaignId) {
        const address = `http://localhost:5264/Campaign/AddCard/${campaignId}`;
        const response = await fetch(address, { method: 'Get' });
        const result = await response.text();
        console.log(result);
        $("#campainCardsDiv").append(result);


    }
    function _clearErrorMessages() {
        let spans = document.querySelectorAll('span[data-valmsg-for]');
        spans.forEach(s => {
            s.textContent = "";
        });
    }
})()