function generateDataset(question) {
    let dataset = {
        label: "",
        data: 0,
        borderWidth: 1,
        backgroundColor: [],
        borderColor: []
    };

    for (let option of question.options) {
        dataset.label = option;
        for (let answer of question.answers) {
            console.log("aa");
            if (answer.text === option) {
                dataset.data++;
            }
        }
        dataset.backgroundColor.push("#" + Math.floor(Math.random() * 16777215).toString(16));
        dataset.borderColor.push("#000000");
    }


    return dataset;
}