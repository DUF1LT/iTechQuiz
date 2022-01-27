const singleAnswerOption = document.getElementById("singleAnswerOption");
const multipleAnswerOption = document.getElementById("multipleAnswerOption");
const textAnswerOption = document.getElementById("textAnswerOption");
const fileAnswerOption = document.getElementById("fileAnswerOption");
const starAnswerOption = document.getElementById("starAnswerOption");
const scaleAnswerOption = document.getElementById("scaleAnswerOption");

const insertQuestion = htmlToElement(`<div class="new-survey-main-constructor__insert">INSERT QUESTION HERE</div>`);
const singleAnswerHtml = 
    `<div class="new-survey-main__single-answer">
    <div class="new-survey-main__single-answer__title" >
    <span>1.</span>
    <input type="text" value="Question with single answer"/>
    </div>
    <div class="new-survey-main__single-answer__answers">
    <div class="new-survey-main__single-answer__option">
    <input type="radio" name="singleAnswer" />
    <input class="new-survey-main__single-answer__option__text-input" type="text" value="Answer 1" />
    </div>
    <div class="new-survey-main__single-answer__option">
    <input type="radio" name="singleAnswer" />
    <input class="new-survey-main__single-answer__option__text-input" type="text" value="Answer 2" />
    </div>
    <div class="new-survey-main__single-answer__option">
    <input type="radio" name="singleAnswer" />
    <input class="new-survey-main__single-answer__option__text-input" type="text" value="Answer 3" />
    </div>
    </div>
    </div>`;

function htmlToElement(html) {
    var template = document.createElement('template');
    html = html.trim();
    template.innerHTML = html;
    return template.content.firstChild;
}

function dragStart(html) {
    event.dataTransfer.setData("text/html", singleAnswerHtml);
    console.log(event);
    dropZone.appendChild(insertQuestion);
}

const dropZone = document.getElementById("surveyConstructor");

dropZone.addEventListener("dragover", event => {
    event.preventDefault();
});

dropZone.addEventListener("drop", event => {
    event.preventDefault();
    const data = event.dataTransfer.getData("text/html");
    dropZone.appendChild(htmlToElement(data));
});

/*

singleAnswerOption.addEventListener("dragstart", event => {
    event.dataTransfer.setData("text/html", singleAnswerHtml);
    let img = new Image();
    console.log(event);
    dropZone.appendChild(insertQuestion);
});

singleAnswerOption.addEventListener("dragend", event => {
    dropZone.removeChild(insertQuestion);
});

*/




