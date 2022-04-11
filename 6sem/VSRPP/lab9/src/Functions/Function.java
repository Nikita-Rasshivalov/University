package Functions;

import javax.management.InvalidApplicationException;

public abstract class Function<T> {
    protected T[] _values;
    protected int _current;
    private String _name;

    public String getName() {
        return _name;
    }

    public Function(String name, T[] values) {
        _values = values;
        _name = name;
    }

    public boolean hasNext() {
        if (_current < _values.length) {
            return true;
        }

        return false;
    }

    public abstract T execute() throws InvalidApplicationException;
}