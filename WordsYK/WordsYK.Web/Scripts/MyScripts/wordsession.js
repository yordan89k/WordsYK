
function CustomValidation() {
    this.invalidities = [];
    this.validityChecks = [];
}

CustomValidation.prototype = {
    addInvalidity: function (message) {
        this.invalidities.push(message);
    },
    getInvalidities: function () {
        return this.invalidities.join('. \n');
    },
    checkValidity: function (input) {

        for (var i = 0; i < this.validityChecks.length; i++) {

            var isInvalid = this.validityChecks[i].isInvalid(input);
            console.log("isInvalid: " + isInvalid)
            if (isInvalid) {
                this.addInvalidity(this.validityChecks[i].invalidityMessage);
            }

            if (input && input.value.length > 2) {
                if (isInvalid) {
                    input.classList.add('invalid');
                    input.classList.remove('valid');

                } else {
                    input.classList.remove('invalid');
                    input.classList.add('valid');

                }

            }
            updateResult();
        } // end for
    }
};

var answerValidityChecks = [
    {
        isInvalid: function (input) {
            return input.value !== input.dataset.answer;
        },
        invalidityMessage: 'Example invalidity message',
        element: answerInput
    }
];

function checkInput(input) {

    input.CustomValidation.invalidities = [];
    input.CustomValidation.checkValidity(input);

    if (input.CustomValidation.invalidities.length == 0 && input.value != '') {
        console.log("no invalidities found !!!!!! YESSSS")
        input.setCustomValidity('');
    } else {
        var message = input.CustomValidation.getInvalidities();
        console.log("Invalid as fuck")
        input.setCustomValidity(message);
    }
}

var inputs = document.querySelectorAll('input:not([type="submit"])');

for (var i = 0; i < inputs.length; i++) {
    inputs[i].addEventListener('keyup', function () {
        this.CustomValidation = new CustomValidation();
        this.CustomValidation.validityChecks = answerValidityChecks;
        checkInput(this);
    });
}

function updateResult() {

    var correctWordsNumber = document.getElementsByClassName("valid").length;
    document.getElementById("result-progress").innerHTML = correctWordsNumber;

    if (correctWordsNumber == numberOfWords) {
        loadMoreBox.classList.remove('hidden')
        resultInfo.classList.add('greenText')
        loadMoreBtn.focus();
    } else {
        loadMoreBox.classList.add('hidden')
        resultInfo.classList.remove('greenText')
    }
};