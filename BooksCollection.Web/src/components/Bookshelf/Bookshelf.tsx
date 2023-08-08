import React from "react";
import BookshelfRow from "../BookshelfRow/BookshelfRow";
import BookshelfActions from "../BookshelfActions/BookshelfActions";
import "./Bookshelf.less";

const Bookshelf = () => (
  <div className="bookshelf">
    <div className="bookshelfActionsContainer">
      <BookshelfActions />
    </div>
    <div className="bookshelfRowContainer">
      <BookshelfRow/>
    </div>
    <div className="bookshelfRowContainer">
    <BookshelfRow/>
    </div>
    <div className="bookshelfRowContainer">
    <BookshelfRow/>
    </div>
    {/* {Array.from(=> (
      <BookshelfRow key={i} />
    ))} */}
  </div>
);

export default Bookshelf;
