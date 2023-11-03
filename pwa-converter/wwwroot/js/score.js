const auditResultTitles = document.getElementsByClassName("audit-result-title")

Array.prototype.forEach.call(auditResultTitles, (title) => {
    title.addEventListener("click", () => {
        if (title.nextElementSibling !== null) {
            if (title.nextElementSibling.classList.contains("hidden")) {
                title.nextElementSibling.classList.remove("hidden")
            } else {
                title.nextElementSibling.classList.add("hidden")
            }
        }
    })
})