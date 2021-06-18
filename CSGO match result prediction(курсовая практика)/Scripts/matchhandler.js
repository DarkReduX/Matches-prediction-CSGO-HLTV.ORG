props = {
    wasInitializedAnimation: false
}
var slider ;
document.addEventListener('DOMContentLoaded', () => {
    var items = document.getElementsByClassName('dot');
    initPagginationOnClickEvent(items);
    slider = document.getElementsByClassName("slider_cont")[0];
})



function initPagginationOnClickEvent(items) {
    for (var i = 0; i < items.length; i++) {
        items[i].onclick = goToPage;
    }
    document.getElementsByClassName('next')[0].onclick = goNextPage;
    document.getElementsByClassName('prev')[0].onclick = goPrevPage;
}

function goToPage() {
    var pageNumber = this.getAttribute('data-page');
    var activeDot = document.getElementsByClassName('dot active')[0];
    var currentPage = document.getElementsByClassName("active_page")[0];
    var goToPageItem = document.getElementsByClassName("page")[pageNumber - 1];
    currentPage.className = "page active_page images_active remove_active";
    slider.style.transform = 'translateX(-' + (pageNumber - 1) * 10 + '%)';
    goToPageItem.className = "page active_page images_active";
    setTimeout(() => {
        currentPage.className = "page";}, 1000)
    this.className = "dot active";
    activeDot.className = "dot";
}

function goNextPage() {
    var activeDot = document.getElementsByClassName('dot active')[0];
    var dots = document.getElementsByClassName('dot');
    var currentPageNumber = activeDot.getAttribute('data-page') - 1;
    var currentPage = document.getElementsByClassName("active_page")[0];
    var goToPageItem;
    var pages = document.getElementsByClassName('page');
    if (currentPageNumber == pages.length-1) {
        goToPageItem = document.getElementsByClassName("page")[0]
        currentPage.className = "page active_page images_active remove_active";
        slider.style.transform = 'translateX(-' + (0) * 10 + '%)';
        goToPageItem.className = "page active_page images_active";
        setTimeout(() => {
            currentPage.className = "page";
        }, 75)
        dots[0].className = "dot active";
        activeDot.className = "dot";
    }
    else {
        goToPageItem = document.getElementsByClassName("page")[currentPageNumber + 1]
        currentPage.className = "page active_page images_active remove_active";
        slider.style.transform = 'translateX(-' + (currentPageNumber+1) * 10 + '%)';
        goToPageItem.className = "page active_page images_active";
        setTimeout(() => {
            currentPage.className = "page";
        }, 75)
        dots[currentPageNumber+1].className = "dot active";
        activeDot.className = "dot";
    }
}

function goPrevPage() {
    var activeDot = document.getElementsByClassName('dot active')[0];
    var dots = document.getElementsByClassName('dot');
    var currentPageNumber = activeDot.getAttribute('data-page') - 1;
    var currentPage = document.getElementsByClassName("active_page")[0];
    var goToPageItem;
    var pages = document.getElementsByClassName('page');
    if (currentPageNumber == 0) {
        goToPageItem = document.getElementsByClassName("page")[pages.length-1]
        currentPage.className = "page active_page images_active remove_active";
        slider.style.transform = 'translateX(-' + (pages.length-1) * 10 + '%)';
        goToPageItem.className = "page active_page images_active";
        setTimeout(() => {
            currentPage.className = "page";
        }, 1000)
        dots[pages.length-1].className = "dot active";
        activeDot.className = "dot";
    }
    else {
        goToPageItem = document.getElementsByClassName("page")[currentPageNumber - 1]
        currentPage.className = "page active_page images_active remove_active";
        slider.style.transform = 'translateX(-' + (currentPageNumber - 1) * 10 + '%)';
        goToPageItem.className = "page active_page images_active";
        setTimeout(() => {
            currentPage.className = "page";
        }, 1000)
        dots[currentPageNumber - 1].className = "dot active";
        activeDot.className = "dot";
    }
}

function SetActive(page, dot) {
    page.className = "page active_page images_active";
    page.style.display = 'block';
    dot.className = "dot active";
}

function SetNotActive(page, dot) {
    page.className = "page";
    page.style.display = "none";
    dot.className = "dot";
}