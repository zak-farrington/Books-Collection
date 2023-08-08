import React from "react";
import { useAppDispatch } from "../../store/store"
import { getBooksList } from "../../store/booksSlice";
import { Button } from "react-bootstrap";

import "./BookshelfActions.less";

const BookshelfActions = () => {
  const dispatch = useAppDispatch();

  const handleRefresh = () => {
    dispatch(getBooksList());
  };

  return (
    <div className="bookshelfButtonActions">
      <Button onClick={() => {/* Add functionality for adding a book */}}><i className="bi bi-plus-circle"></i> Add To Collection</Button>
      <Button onClick={handleRefresh}><i className="bi bi-arrow-clockwise"></i> Refresh Books</Button>
    </div>
  );
};

export default BookshelfActions;
