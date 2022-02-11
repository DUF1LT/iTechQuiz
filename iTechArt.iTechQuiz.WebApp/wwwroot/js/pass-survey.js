Vue.component('survey-page',
    {
        props: {
            page: Object,
            survey: Object
        },
        created: function () {
            if (this.survey.hasRandomSequence) {
                this.page.questions.sort(() => Math.random() - Math.random());
            }
        },
        template:
            '<div class="pass-survey__page">' +
                '<span>{{page.name}}</span>' +
                '<page-question-random v-if="survey.hasRandomSequence" v-for="(randomQuestion, index) in page.questions" v-bind:question="randomQuestion" :survey="survey" :index="index" v-bind:ref="randomQuestion.id"></page-question-random>' +
                '<page-question v-if="!survey.hasRandomSequence" v-for="question in page.questions" v-bind:question="question" :survey="survey" v-bind:ref="question.id"></page-question>' +
            '</div>'
    });

Vue.component('page-question',
    {
        props: {
            question: Object,
            survey: Object,
            alert: false
        },
        methods: {
            alertRequired: function () {
                this.alert = true;
            },
            alertDisable: function () {
                this.alert = false;
            }
        },
        template:
            '<div class="pass-survey__question" v-on:click="alertDisable()">' +
                '<hr v-if="question.number != 1"/>' +
                '<div class="pass-survey__question-title">' +
                    '<span v-if="survey.hasQuestionNumeration">{{question.number}}.</span>' +
                    '<span>{{question.content}}</span>' +
                    '<img v-if="question.isRequired && survey.renderStarsAtRequiredFields" src="/img/star.svg" width="15"/>' +
                '</div>' +
                '<component v-bind:is="question.type" v-bind:question="question"></component>' +
                '<span class="pass-survey__alert-question" v-if="alert">You should answer this question!</span>' +
            '</div >'
    });

Vue.component('page-question-random',
    {
        props: {
            question: Object,
            survey: Object,
            index: Number,
            alert: false
        },
        methods: {
            alertRequired: function () {
                this.alert = true;
            },
            alertDisable: function() {
                this.alert = false;
            }
        },
        template:
            '<div class="pass-survey__question" v-on:click="alertDisable()">'+
                '<hr v-if="(index + 1) != 1"/>' +
                '<div class="pass-survey__question-title">' +
                    '<span v-if="survey.hasQuestionNumeration">{{index + 1}}.</span>' +
                    '<span>{{question.content}}</span>' +
                    '<img v-if="question.isRequired" src="/img/star.svg" width="15"/>' +
                '</div>' +
                '<component v-bind:is="question.type" v-bind:question="question"></component>' +
                '<span class="pass-survey__alert-question" v-if="alert">You should answer this question!</span>' +
            '</div >'
    });

Vue.component('SingleAnswer',
    {
        props: {
            question: Object
        },
        template:
            '<div class="pass-survey__question-answers">' +
                '<single-answer-option v-for="(option, index) in question.options" v-bind:option="option" :question="question" :index="index"></single-answer-option>' +
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
            '<div class="pass-survey__question-option">' +
                '<input type="radio" v-model="question.answer.text" v-bind:value="option"/>' +
                '<span>{{option}}</span>' +
            '</div>'
    });

Vue.component('MultipleAnswer',
    {
        props: {
            question: Object
        },
        template:
            '<div class="pass-survey__question-answers">' +
                '<multiple-answer-option v-for="(option, index) in question.options" v-bind:option="option" :question="question" :index="index"></multiple-answer-option>' +
            '</div>'
    });

Vue.component('multiple-answer-option',
    {
        props: {
            index: Number,
            option: String,
            question: Object
        },
        template:
            '<div class="pass-survey__question-option">' +
                '<input type="checkbox" v-model="question.answer.multipleAnswer" v-bind:value="option"/>' +
                '<span>{{option}}</span>' +
            '</div>'
    });

Vue.component('TextAnswer',
    {
        props: {
            question: Object
        },
        template: '<textarea v-model="question.answer.text"/>'
    });

Vue.component('File',
    {
        props: {
            question: Object
        },
        methods: {
            inputFile: function() {
                const reader = new FileReader();

                reader.readAsDataURL(this.$refs[`${this.question.id}__file`].files[0]);
                reader.onload = (evt) => {
                    this.question.answer.file.byteArray = reader.result.split(",")[1];
                    this.question.answer.file.name = this.$refs[`${this.question.id}__file`].files[0].name;
                    this.question.answer.file.type = this.$refs[`${this.question.id}__file`].files[0].type;
                }
            }
        },
        template:
            '<input width="50px" type="file" v-on:change="inputFile()" v-bind:ref="question.id + `__file`"/>'
        });

Vue.component('Rating',
    {
        props: {
            question: Object
        },
        template:
            '<div class="pass-survey__question-rating">' +
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
            rateStar: function (index) {
                this.question.answer.numeric = index;
            }
        },
        template:
            '<i class="fa-star fa-lg" v-bind:class="{far: index > question.answer.numeric, fas: index <= question.answer.numeric}" v-on:click="rateStar(index)"></i>'
    });

Vue.component('Scale',
    {
        props: {
            question: Object
        },
        template:
            '<div class="pass-survey__question-scale">' +
                '<span style="align-self: end;">0</span>' +
                '<div class="pass-survey__question-scale-input">' +
                    '<span>{{question.answer.numeric}}</span>' +
                    '<input type="range" min="1" max="100" value="0" width="200px" v-model="question.answer.numeric"/>' +
                '</div>' +
                '<span style="align-self: end;">100</span>' +
            '</div>'
    });
