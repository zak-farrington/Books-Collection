// index.tsx
import React from "react";
import ReactDOM from "react-dom";
import { Provider } from "react-redux";
import { store } from "./store/store";
import Bookshelf from "./components/Bookshelf/Bookshelf";
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap-icons/font/bootstrap-icons.css";
import "./index.less";

ReactDOM.render(
  <Provider store={store}>
    <div className="bookshelfContainer">
      <Bookshelf />
    </div>
  </Provider>,
  document.getElementById("root")
);