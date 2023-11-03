const divImageSelected = document.getElementById("image-chosen")
const inputImageUpload = document.getElementById("Images")
const btnOptimise = document.getElementById("btn-optimise")
const spanImagesErrorMessage = document.getElementById("images-error-message")
let imagesChosen = [];
const userIdAtLoad = getCookieValue("pwa-converter-image-optimisation-user-id");
inputImageUpload.addEventListener("change", () => {
    Array.prototype.forEach.call(inputImageUpload.files, image => imagesChosen.push(image.name))

    updateDivTag()
})

btnOptimise.addEventListener("click", () => {
    setInterval(() => {
        const userId = getCookieValue("pwa-converter-image-optimisation-user-id")

        if (userId != userIdAtLoad) {
            fetch(`${window.location.origin}/images/CleanUp/${userId}`)
            location.replace(`${window.location.origin}/images/Optimise`)
        }
    }, 1000)
})

function getCookieValue(name) {
    return document.cookie
        .split("; ")
        .find((row) => row.startsWith(`${name}=`))
        ?.split("=")[1];
} 

const updateDivTag = () => {
    divImageSelected.innerHTML = ""
    imagesChosen.forEach((image) => {
        const imageName = document.createTextNode(image)

        const newDivTag = document.createElement("div")

        newDivTag.appendChild(imageName)

        newDivTag.className = "border-[1px] border-primary text-primary px-4 rounded-md flex gap-2 items-center"

        divImageSelected.appendChild(newDivTag)

        imagesChosen = []
    });
}