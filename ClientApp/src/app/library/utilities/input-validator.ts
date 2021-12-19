export class InputValidator {

    constructor(inputType:InputType, value:any){
        
        this._inputType = inputType;
        this._value = value;

    }

    private _inputType: InputType;
    private _isValid: boolean = false;
    private _value: any;

    //Bootstrap class 'is-valid' or 'is-invalid'.
    public ClassValue: string = ''
    public ResponseMessage: string = '';

    public get Value(){
        return this._value;
    }
    public set Value(value:any){
               
        this._value = value;
        this._isValid = this.validateInput();
        
        if(this._isValid){
            this.ClassValue = 'text-success';
        } else{
            this.ClassValue = 'text-alert'
        }
    }
    public get IsValid(){
        return this._isValid;
    } 
    
    validateInput(){
        
        //Name validation
        if (this._inputType == InputType.Name){
            
            let name = this.Value as string
            if(name.length < 3){

                this.ResponseMessage = "Name must be atleast 3 characters long."
                return false;
            }
        }
        //Email validation
        if (this._inputType == InputType.Email){
            
            let email = this.Value as string
            const regularExpression = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

            if(email == "" || !regularExpression.test(String(email).toLowerCase())){
                
                this.ResponseMessage = "Please enter a valid email."
                return false;
            }
        }
        //Password
        if (this._inputType == InputType.Password){
            
            const password = this.Value as string
            const minNumberofChars = 8;
            const maxNumberofChars = 16;
            const regularExpression = /^(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{6,16}$/;

            if(password.length < minNumberofChars){
                this.ResponseMessage = "Password must be atleast 8 characters long." 
                return false;
            }

            if(password.length > maxNumberofChars){
                this.ResponseMessage = "Password cannot be more that 16 characters long." 
                return false;
            }

            if(!regularExpression.test(password)) {
                this.ResponseMessage = "Password should contain atleast one number and one special character.";
                return false;
            }

        }
        
        //Age
        if(this._inputType == InputType.Age){
            
            let age = this._value

            if(age < 6){
                this.ResponseMessage = "We seriously doudt your under the age of six.";
                return false;
            }
            if(age < 80 ){
                this.ResponseMessage = "We seriously doudt your over the age of 80.";
                return false;
            }

        }

        // phone
        if(this._inputType == InputType.Phone){

            let phone = this._value as string
            if(!phone.startsWith("0")){
                this.ResponseMessage = "Phone number needs to start with '0'.";
                return false;
            }
            if(phone.length != 10){
                this.ResponseMessage = "Phone number needs to have 10 digits.";
                return false;
            }
        }

        if(this._inputType == InputType.General){
            
            let input = this.Value as string;
            if(input.length < 5){

                this.ResponseMessage = "Surely that can't be all.";
                return false;

            }
        }

        if(this._inputType == InputType.Currency){
            // const regularExpression = /^\d+(?:\.\d{0,2})/;
            // var numStr = "123.20";
            // if (!regularExpression.test(this._value))
            //     this._value = "00.00"
        }

        this.ResponseMessage = "All good.";
        return true;

    }


}

export enum InputType{
    Name = 0, Email = 1, Password = 3 , Age = 4 , Phone = 5, General = 6, Currency = 7
}
