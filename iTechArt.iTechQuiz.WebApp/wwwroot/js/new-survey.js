Vue.component('survey-page-tab',
    {
        props: {
            page : Object,
            currentPage: Object
        },
        template:
            '<input type="button" v-if="page == currentPage" class="red-solid-button" v-model="page.name" />' +
            '<input type="button" v-else class="transparent-button" v-model="page.name" v-on:click="$emit(`change-page`, page)"/>'
    });

Vue.component('survey-page',
    {
        props: {
            page: Object,
            survey: Object
        },
        methods: {
            removeQuestion: function(question){
                let questionIndex = this.page.questions.indexOf(question);

                for (let i = questionIndex; i < this.page.questions.length; i++) {
                    this.page.questions[i].number--;
                }

                this.page.questions.splice(questionIndex, 1);
            },
            upQuestion: function (question) {
                let questionIndex = this.page.questions.indexOf(question);

                let newNumber = this.page.questions[questionIndex - 1].number;
                this.page.questions[questionIndex - 1].number = question.number;
                question.number = newNumber;

                this.page.questions.sort(function(a, b) {
                    if (a.number > b.number) {
                        return 1;
                    }
                    if (a.number < b.number) {
                        return -1;
                    }

                    return 0;
                });
            },
            downQuestion: function (question) {
                let questionIndex = this.page.questions.indexOf(question);

                let newNumber = this.page.questions[questionIndex + 1].number;
                this.page.questions[questionIndex + 1].number = question.number;
                question.number = newNumber;

                this.page.questions.sort(function (a, b) {
                    if (a.number > b.number) {
                        return 1;
                    }
                    if (a.number < b.number) {
                        return -1;
                    }

                    return 0;
                });
            }
        },
        template:
            '<div class=new-survey-main-constructor-wrapper>' +
                '<div id="surveyConstructor" class="new-survey-main-constructor">' +
                    '<div class=new-survey-main__page-title>' +
                        '<input v-model="page.name" type="text" />' +
                        '<img src="/img/remove.svg" width="20"  v-on:click="$emit(`remove-page`, page)">' +
                    '</div>' +
                    '<page-question v-for="question in page.questions" v-bind:question="question" v-bind:questionsAmount="page.questions.length"' +
                    'v-bind:survey="survey" v-on:remove-question="removeQuestion(question)"' +
                    'v-on:up-question="upQuestion(question)" v-on:down-question="downQuestion(question)">' +
                    '</page-question>' +
                '</div >' +
            '</div>'
    });

Vue.component('page-question',
    {
        props: {
            question: Object,
            questionsAmount: Number,
            survey: Object
        },
        methods: {
            editQuestion: function () {
                this.question.isEditable = true;
                this.question.copy = JSON.parse(JSON.stringify(this.question));
            },
            saveQuestion: function () {
                this.question.isEditable = false;
                this.question.copy = null;
            },
            cancelQuestion: function () {
                this.question.content = this.question.copy.content;
                this.question.isRequired = this.question.copy.isRequired;
                this.question.numericOption = this.question.copy.numericOption;
                this.question.options = JSON.parse(JSON.stringify(this.question.copy.options));
                this.question.textOption = this.question.copy.textOption;
                this.question.numericOption = this.question.copy.numericOption;
                this.question.isEditable = false;
                this.question.copy = null;
            }
        },
        template: 
            `<div class="new-survey-main__question" v-bind:class="{ 'editable-question': question.isEditable }">` +
                '<div class="new-survey-main__editable-question__settings" v-if="question.isEditable">' +
                    '<div class="new-survey-main__editable-question__settings__move" v-if="questionsAmount != 1 ">' +
                        '<div class="new-survey-main__editable-question__settings__move-arrows">' +
                            '<img src="/img/up.svg" width="15" v-if="question.number != 1" v-on:click="$emit(`up-question`, question)"/>' +
                            '<img src="/img/down.svg" width="15" v-if="question.number != questionsAmount" v-on:click="$emit(`down-question`, question)"/>' +
                        '</div>' +
                        '<label>Move</label>' +
                    '</div>' +
                    '<div class="new-survey-main__editable-question__settings__delete-and-require">' +
                        '<div class="new-survey-main__editable-question__settings__require">' +
                            '<input type="checkbox" v-model="question.isRequired" />'+
                            '<span>Is required</span>' +
                        '</div>' +
                        '<img src="/img/remove.svg" width="15" v-on:click="$emit(`remove-question`, question)"/>' +
                    '</div>' +
                '</div>' +
                '<div class="new-survey-main__question__body"> ' +
                    '<div class="new-survey-main__question__header">' +
                        '<div class="new-survey-main__question__title" v-bind:class="{width100: question.isEditable}" >' +
                            '<span v-if="survey.hasQuestionNumeration">{{question.number}}.</span>' +
                            '<input type="text" v-model="question.content" v-if="question.isEditable"/>' +
                            '<span v-else>{{question.content}}</span>' +
                            '<img v-if="survey.renderStarsAtRequiredFields && question.isRequired && !question.isEditable" width="15" src="/img/star.svg" />' +
                        '</div>' +
                    '<span class="new-survey-main__question-edit" v-if="!question.isEditable" v-on:click="editQuestion(question)">Edit</span>' +
                    '</div>' +
                    '<component v-bind:is="question.type" :question="question"></component>' +
                '</div>' +
                '<div class="new-survey-main__editable-question__buttons" v-if="question.isEditable">' +
                    '<input type="button" class="button" value="Save" v-on:click="saveQuestion()" />' +
                    '<input type="button" class="red-button" value="Cancel" v-on:click="cancelQuestion()" />' +
                '</div>' +
            '</div>'
    });

Vue.component('singleAnswerQuestion',
    {
        props: {
            question: Object
        },
        methods: {
            addOption: function () {
                this.question.options.push("Answer " + (this.question.options.length + 1));
            }
        },
        template:
            '<div class="new-survey-main__question__answers">' +
                '<single-answer-option v-for="(option, index) in question.options" v-if="question.type == `singleAnswerQuestion`" v-bind:option="option" :question="question" :index="index"></single-answer-option>' +
                '<input type="button" class="new-survey-main__question-add-option" value="Add option" v-if="question.isEditable" v-on:click="addOption()"/>' +
            '</div>'
    });

Vue.component('single-answer-option',
    {
        props: {
            index: Number,
            option: String,
            question: Object
        },
        methods: {
            removeQuestionOption: function() {
                this.question.options.splice(this.index, 1);
            }
        },
        template:
            '<div class="new-survey-main__question__option">' +
                '<div class="new-survey-main__question__option-input">' +
                    '<input type="radio" v-bind:name="question.number + `__` + question.content" />' +
                    '<input class="new-survey-main__question__option__text-input" type="text" v-if="question.isEditable" v-model="question.options[index]"/>' +
                    '<span class="new-survey-main__question__option__text-input" v-else>{{ option }}</span>' +
                '</div>' +
                '<img src="/img/remove.svg" width="10" v-if="question.isEditable" v-on:click="removeQuestionOption()"/>' +
            '</div>'
    });

Vue.component('multipleAnswerQuestion',
    {
        props: {
            question: Object
        },
        methods: {
            addOption: function () {
                this.question.options.push("Answer " + (this.question.options.length + 1));
            }
        },
        template:
                '<div class="new-survey-main__question__answers">' +
                    '<multiple-answer-option v-for="(option, index) in question.options" v-bind:option="option" :question="question" :index="index"></multiple-answer-option>' +
                    '<input type="button" class="new-survey-main__question-add-option" value="Add option" v-if="question.isEditable" v-on:click="addOption()"/>' +
                '</div>'
    });

Vue.component('multiple-answer-option',
    {
        props: {
            index: Number,
            option: String,
            question: Object
        },
        methods: {
            removeQuestionOption: function() {
                this.question.options.splice(this.index, 1);
            }
        },
        template:
            '<div class="new-survey-main__question__option">' +
                '<div class="new-survey-main__question__option-input">'+
                    '<input type="checkbox" />' +
                    '<input class="new-survey-main__question__option__text-input" type="text" v-if="question.isEditable" v-model="question.options[index]"/>' +
                    '<span class="new-survey-main__question__option__text-input" v-else>{{ option }}</span>' +
                '</div>' +
                '<img src="/img/remove.svg" width="10" v-if="question.isEditable" v-on:click="removeQuestionOption()"/>' +
            '</div>'
    });

Vue.component('textQuestion',
    {
        props: {
            question: Object
        },
        template: '<textarea v-model="question.option"/>'
    });

Vue.component('fileQuestion',
    {
        props: {
            question: Object
        },
        template: '<input width="50px" type="file"/>'
    });

Vue.component('starQuestion',
    {
        props: {
            question: Object
        },
        template:
            '<div class="new-survey-main__question-rating">' +
                '<star-question-option v-for="index in [1,2,3,4,5]" v-bind:question="question" v-bind:index="index"></star-question-option>' +
            '</div>'
    });

Vue.component('star-question-option',
    {
        props: {
            question: Object,
            index: Number
        },
        methods: {
            rateStar: function(index) {
                this.question.numericOption = index;
            }
        },
        template:
            '<i class="fa-star fa-lg" v-bind:class="{far: index > question.numericOption, fas: index <= question.numericOption}" v-on:click="rateStar(index)"></i>'
    });

Vue.component('scaleQuestion',
    {
        props: {
            question: Object
        },
        template:
            '<div class="new-survey-main__question-scale">' +
                '<span style="align-self: end;">0</span>' +
                    '<div class="new-survey-main__question-scale-input">' +
                        '<span>{{question.numericOption}}</span>' +
                        '<input type="range" min="0" max="100" value="0" width="200px" v-model="question.numericOption"/>' +
                    '</div>' +
                '<span style="align-self: end;">100</span>' +
            '</div>'
    });