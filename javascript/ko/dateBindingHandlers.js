        ko.bindingHandlers.dateStringVal = {
            update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
                var value = valueAccessor(), allBindings = allBindingsAccessor();
                //console.log(value());
                var valueUnwrapped = new Date(ko.utils.unwrapObservable(value));
                var pattern = allBindings.datePattern || 'MM/dd/yyyy';

                if ($(element).val !== undefined && $(element).val !== null
                        && value !== undefined && value() !== null)
                    $(element).val(valueUnwrapped.toString(pattern));
                else
                    $(element).val('');
            }
        }

        ko.bindingHandlers.dateStringText = {
            update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
                var value = valueAccessor(), allBindings = allBindingsAccessor();
                var valueUnwrapped = new Date(ko.utils.unwrapObservable(value));
                var pattern = allBindings.datePattern || 'MM/dd/yyyy';

                if ($(element).text !== undefined && $(element).text !== null
                        && value !== undefined && value() !== null)
                    $(element).text(valueUnwrapped.toString(pattern));
                else
                    $(element).text('');
            }
        }
