const userId = getCookieValue("pwa-converter-user-id")
if (userId !== undefined && userId !== sessionStorage.getItem("previous-user-id")) {
    fetch(`${window.location.origin}/Pwa/CleanUp/${userId}`)
    sessionStorage.setItem("previous-user-id", userId)
}

const downloadLink = document.getElementById("download-link")
downloadLink.addEventListener("click", () => setInterval(() => location.reload(), 1000))

const btnCopy = document.getElementById("btnCopy")

function getCookieValue(name) {
    return document.cookie
        .split("; ")
        .find((row) => row.startsWith(`${name}=`))
        ?.split("=")[1];
} 

btnCopy.addEventListener("click", () => {
    const textToCopy = document.getElementById("htmlCode")

    navigator.clipboard.writeText(decodeToHTML(textToCopy))
    alert("Code copied to the clipboard");
})

const decodeToHTML = textToDecode => {
    var textArea = document.createElement('textarea')
    textArea.innerHTML = textToDecode.innerHTML
    return textArea.value
}