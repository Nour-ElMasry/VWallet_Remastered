class DateService{
    getAge = (date) => {
        var today = new Date();
        var birthDate = new Date(date);
        var age = today.getFullYear() - birthDate.getFullYear();
        var m = today.getMonth() - birthDate.getMonth();
        if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) 
        {
            age--;
        }
        return age;
    }
    
    dateLongFormat = (date) => {
      var options = { year: 'numeric', month: 'short', day: 'numeric' };
      var today  = new Date(date);

      return today.toLocaleDateString("en-US", options);
    }

    getDateAndAge = (date) => {
        var dateFormat = this.dateLongFormat(date);
        var age = this.getAge(date);
        return {
            date: dateFormat,
            age: age,
        };
    }
}

export default new DateService();