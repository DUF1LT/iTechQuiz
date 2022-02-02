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
            page: Object
        },
        template:
            '<div class=new-survey-main-constructor-wrapper>' +
                '<div id="surveyConstructor" class="new-survey-main-constructor">' +
                    '<div class=new-survey-main__page-title>' +
                        '<input v-model="page.name" type="text" />' +
                        '<img src="/img/delete.svg" width="25" v-on:click="$emit(`remove-page`, page)">' +
                    '</div>' +
                    '<page-question v-for="question in page.questions" v-bind:question="question" :questionCopy="JSON.parse(JSON.stringify(question))" v-on:remove-question="$emit(`remove-question`, $event)"></page-question>' +
                '</div >' +
            '</div>'
    });

Vue.component('page-question',
    {
        props: {
            question: Object,
            questionCopy: Object
        },

        template: 
            `<div class="new-survey-main__question" v-bind:class="{ 'editable-question': question.isEditable }">` +
                '<div class="new-survey-main__editable-question__settings" v-if="question.isEditable">' +
                    '<div class="new-survey-main__editable-question__settings__move">' +
                        '<div class="new-survey-main__editable-question__settings__move-arrows">' +
                            '<img src="/img/up.svg" width="15" />' +
                            '<img src="/img/down.svg" width="15" />' +
                        '</div>' +
                            '<label>Move</label>' +
                    '</div>' +
                    '<div class="new-survey-main__editable-question__settings__delete-and-require">' +
                        '<div class="new-survey-main__editable-question__settings__require">' +
                            '<label></label>' +
                        '</div>' +
                        '<img src="/img/remove.svg" width="15" v-on:click="$emit(`remove-question`, question)"/>' +
                    '</div>' +
                '</div>' +
                '<component v-bind:is="question.type" :question="question"></component>' +
                '<div class="new-survey-main__editable-question__buttons" v-if="question.isEditable">' +
                    '<input type="button" class="button" value="Save" v-on:click="question.isEditable = false" />' +
                    '<input type="button" class="red-button" value="Cancel"  v-on:click="{question.isEditable = false; question = _.cloneDeep(questionCopy)}"/>' +
                '</div>' +
            '</div>'
    });

Vue.component('singleAnswerQuestion',
    {
        props: {
            question: Object
        },
        template:
            '<div class="new-survey-main__question__body"> ' +
                '<div class="new-survey-main__question__header">' +
                    '<div class="new-survey-main__question__title">' +
                        '<span>{{question.number}}.</span>' +
                        '<input type="text" v-model="question.content" v-if="question.isEditable" />' +
                        '<span v-else>{{question.content}}</span>' +
                    '</div>' +
                    '<img src="/img/edit.svg" width="20" v-if="!question.isEditable" v-on:click="question.isEditable = true">' +
                '</div>' +
                '<div class="new-survey-main__question__answers">' +
                    '<single-answer-option v-for="(option, index) in question.options" v-bind:option="option" :question="question" :index="index"></single-answer-option>' +
                '</div>' +
            '</div>'
    });

Vue.component('single-answer-option',
    {
        props: {
            index: Number,
            option: String,
            question: Object
        },
        template:
            '<div class="new-survey-main__question__option">' +
                '<input type="radio" v-bind:name="question.number + `__` + question.content" />' +
                '<input class="new-survey-main__question__option__text-input" type="text" v-if="question.isEditable" v-model="question.options[index]"/>' +
                '<span class="new-survey-main__question__option__text-input" v-else>{{ option }}</span>' +
            '</div>'
    });



