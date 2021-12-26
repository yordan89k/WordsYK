
console.log("TEST big time");

/* ----------------------------

    CustomValidation prototype

    - Keeps track of the list of invalidity messages for this input
    - Keeps track of what validity checks need to be performed for this input
    - Performs the validity checks and sends feedback to the front end
	
---------------------------- */

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

            var requirementElement = this.validityChecks[i].element;
            console.log("requirementElement: " + requirementElement)
            console.log(" requirementElement.value.length: " + requirementElement.value.length)
            if (requirementElement && requirementElement.value.length > 4) {
                if (isInvalid) {
                    requirementElement.classList.add('invalid');
                    requirementElement.classList.remove('valid');
                } else {
                    requirementElement.classList.remove('invalid');
                    requirementElement.classList.add('valid');
                }

            } // end if requirementElement
        } // end for
    }
};



/* ----------------------------

    Validity Checks

    The arrays of validity checks for each input
    Comprised of three things
        1. isInvalid() - the function to determine if the input fulfills a particular requirement
        2. invalidityMessage - the error message to display if the field is invalid
        3. element - The element that states the requirement
	
---------------------------- */

var answerValidityChecks = [
    {
        isInvalid: function (input) {
            return input.value !== correctAnswer;
        },
        invalidityMessage: 'This input needs to be at least 3 characters. Correct answer: ' + correctAnswer,
        element: answerInput
    },
    //{
    //	isInvalid: function (input) {
    //		var illegalCharacters = input.value.match(/[^a-zA-Z0-9]/g);
    //		return illegalCharacters ? true : false;
    //	},
    //	invalidityMessage: 'Only letters and numbers are allowed',
    //	//element: document.querySelector('label[for="answer"] .input-requirements li:nth-child(2)')
    //}
];


/* ----------------------------

    Check this input

    Function to check this particular input
    If input is invalid, use setCustomValidity() to pass a message to be displayed

---------------------------- */

function checkInput(input) {

    console.log("checking input...")
    input.CustomValidation.invalidities = [];
    input.CustomValidation.checkValidity(input);

    if (input.CustomValidation.invalidities.length == 0 && input.value != '') {
        console.log("no invalidities found !!!!!! YESSSS")
        input.setCustomValidity('');
    } else {
        var message = input.CustomValidation.getInvalidities();
        console.log("Invalid as fuck")
        console.log("message: " + message)
        input.setCustomValidity(message);
    }
}



/* ----------------------------

    Setup CustomValidation

    Setup the CustomValidation prototype for each input
    Also sets which array of validity checks to use for that input

---------------------------- */
console.log("answerInput in file: " + answerInput);
answerInput.CustomValidation = new CustomValidation();
answerInput.CustomValidation.validityChecks = answerValidityChecks;




/* ----------------------------

    Event Listeners

---------------------------- */

var inputs = document.querySelectorAll('input:not([type="submit"])');
console.log("inputs: " + inputs);
//var submit = document.querySelector('input[type="submit"');



for (var i = 0; i < inputs.length; i++) {
    inputs[i].addEventListener('keyup', function () {
        checkInput(this);
    });
}

//submit.addEventListener('click', function () {
//	for (var i = 0; i < inputs.length; i++) {
//		checkInput(inputs[i]);
//	}
//});
