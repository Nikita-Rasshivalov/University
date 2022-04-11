package models;

import com.fasterxml.jackson.annotation.JsonIgnore;
import com.fasterxml.jackson.annotation.JsonProperty;
import com.fasterxml.jackson.annotation.JsonTypeInfo;

@JsonTypeInfo(
        use = JsonTypeInfo.Id.CLASS,
        include = JsonTypeInfo.As.EXISTING_PROPERTY,
        property = "@class")
public abstract class Entity {
    private int _id;

    @JsonIgnore
    public int getId() {
        return _id;
    }

    public void setId(int id) {
        _id = id;
    }

    @JsonProperty("@class")
    public String getClassName() {
        return this.getClass().getName();
    }
}
