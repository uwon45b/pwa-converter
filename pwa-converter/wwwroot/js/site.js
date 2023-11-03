// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let cacheContentArray = ['/'];
const cacheContentContainerDiv = document.getElementById("cache-content-container")
const inputCacheContent = document.getElementById("CacheContent")
const btnAddCacheContent = document.getElementById("btn-add-cache-content")
const btnCreateSW = document.getElementById("btn-create-sw")

btnCreateSW.addEventListener("click", () => {
    const cacheContentString = cacheContentArray.toString()
    inputCacheContent.value = cacheContentString
})

UpdateCacheContentContainerDiv()

btnAddCacheContent.addEventListener("click", () => {
    if (inputCacheContent.value !== "" && !cacheContentArray.includes(inputCacheContent.value)) {
        addInputValueToCacheContentArrayAndClearInputField()
        UpdateCacheContentContainerDiv()
    } else {
        inputCacheContent.value = ""
    }
})

inputCacheContent.addEventListener("keypress", function (event) {
    if (event.key === "Enter") {
        event.preventDefault();
        if (inputCacheContent.value !== "" && !cacheContentArray.includes(inputCacheContent.value)) {
            addInputValueToCacheContentArrayAndClearInputField()
            UpdateCacheContentContainerDiv()
        } else {
            inputCacheContent.value = ""
        }
    }
})

function UpdateCacheContentContainerDiv() {
    cacheContentContainerDiv.innerHTML = ""
    cacheContentArray.forEach((content) => {
        const cacheContent = document.createTextNode(content)
        const xSymbol = document.createTextNode("\u00D7")

        const newBtnTag = document.createElement("button")
        const newSpanTag = document.createElement("span");

        newBtnTag.type = "button";

        newBtnTag.appendChild(cacheContent)
        newSpanTag.appendChild(xSymbol)

        newBtnTag.className = "border-[1px] border-primary text-primary px-4 rounded-md flex gap-2 items-center group"
        newSpanTag.className = "group-hover:text-red text-2xl"

        newBtnTag.appendChild(newSpanTag)

        attachEventForDeletionToBtn(newBtnTag)

        cacheContentContainerDiv.appendChild(newBtnTag)
    });
}

function addInputValueToCacheContentArrayAndClearInputField() {
    cacheContentArray.push(inputCacheContent.value)
    inputCacheContent.value = ""
}

function attachEventForDeletionToBtn(btnTag) {
    btnTag.addEventListener("click", () => {
        const valueOfBtnClicked = btnTag.innerHTML
        const addedCacheContent = valueOfBtnClicked.replace(/<span.*<\/span>/, "")
        cacheContentArray = cacheContentArray.filter(arrayElement => arrayElement !== addedCacheContent)
        UpdateCacheContentContainerDiv()
    })
}

