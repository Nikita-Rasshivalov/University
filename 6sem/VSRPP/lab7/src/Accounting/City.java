package Accounting;

import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;

public class City {
    private List<Information> _information = new ArrayList<>();

    public List<Information> getInformation() {
        return _information.stream().collect(Collectors.toUnmodifiableList());
    }

    public void SetStreet(String name) {
        _information.add(new Information(InformationType.Street, name));
    }

    public void SetAvenue(String name) {
        _information.add(new Information(InformationType.Avenue, name));
    }

    public void SetSquare(String name) {
        _information.add(new Information(InformationType.Square, name));
    }

    public class Information {

        private InformationType _type;
        private String _name;

        public Information(InformationType type, String name) {
            _type = type;
            _name = name;
        }

        public InformationType getType() {
            return _type;
        }

        public String toStringRus() {
            return "Тип " + _type + " Название " + _name;
        }
    }
}
