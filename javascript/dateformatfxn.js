 (new Date()).toMSJSON()


      Date.prototype.toMSJSON = function () {
        var date = '\"\\\/Date(' + this.getTime() + '-0500)\\\/\"';
        return date;
      };

