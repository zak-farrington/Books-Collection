import React from "react";
import "./BookshelfRow.less";
import BookThumbnail from "../BookThumbnail/BookThumbnail";

const BookRow = () => (
  <div className="bookshelfRow">
    <BookThumbnail/>
  </div>
);

export default BookRow;
