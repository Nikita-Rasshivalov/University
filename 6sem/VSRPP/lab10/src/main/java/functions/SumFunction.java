package functions;

import javax.management.InvalidApplicationException;

public class SumFunction extends Function<Integer> {

    public SumFunction(Integer... values) {
        super("sum", values);
        _values = values;
    }

    public Integer execute() throws InvalidApplicationException {
        if (_current < _values.length) {
            var result = 0;
            for (var i = 0; i < _values.length; i++) {
                result += Integer.parseInt(_values[i].toString(), 8);
            }
            _current += 1;
            result = Integer.parseInt(Integer.toOctalString(result));
            return result;
        }
        throw new InvalidApplicationException("There is no any input");
    }
}

