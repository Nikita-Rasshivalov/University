import React, { Component } from "react";
import { AppRoutes } from "../../constants/AppRoutes";
import logo from "../../content/logo.png";

export class Header extends Component {
  static displayName = Header.name;

  render() {
    return (
      <header className="header">
        <div className="header-item">
          <a href="/">
            <img className="logo-img" src={logo}></img>
          </a>

          <div className="header-title">
            <h1>Нейронная сеть для распознования огурцов</h1>
          </div>
        </div>

      </header>
    );
  }
}
