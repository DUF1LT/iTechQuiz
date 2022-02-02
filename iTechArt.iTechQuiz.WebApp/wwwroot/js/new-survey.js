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
                    '<div class=new-survey-main-constructor__page-title>' +
                        '<input v-model="page.name" type="text" />' +
                        '<img src="/img/delete.svg" width="25" v-on:click="$emit(`remove-page`, page)">' +
                    '</div>' +
                '</div >' +
            '</div>'
    });

Vue.component('single-answer-question',
    {
        template: "<div>I'm single answer question</div>"
    });
