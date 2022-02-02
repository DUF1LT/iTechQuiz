let survey = new Vue({
    el: '#survey-vue',
    data: {
        survey: {
            "title": null,
            "currentPage": 0,
            "pages": [
                {
                    "name": "Page 1",
                    "questions": []
                }],
            "isAnonymous": false,
            "hasQuestionNumeration": false,
            "hasPagesNumeration": false,
            "hasRandomSequence": false,
            "renderStarsAtRequiredFields": false,
            "hasProgressBar": false
        }
    },
    methods: {
        addPage: function (event) {
            this.survey.pages.push(
                {
                    name: 'Page ' + (this.survey.pages.length + 1),
                    questions: []
                });
        },
        changePage: function (page) {
            this.survey.currentPage = this.survey.pages.indexOf(page);
        }
    }
});

Vue.component('survey-page',
    {
        props: {
            page : Object,
            currentPage: Object
        },
        template: '<input type="button" v-if="page == currentPage" class="red-solid-button" v-model="page.name" />' +
            '<input type="button" v-else class="transparent-button" v-model="page.name" v-on:click="$emit(`change-page`, page)"/>'
    });

Vue.component('single-answer-question',
    {
        template: "<div>I'm single answer question</div>"
    });
