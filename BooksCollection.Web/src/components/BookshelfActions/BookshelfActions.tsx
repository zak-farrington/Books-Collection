import React, { useState } from "react";
import { useAppDispatch } from "../../store/store"
import { getBooksList } from "../../store/booksSlice";
import { Button, Modal } from "react-bootstrap";

import "./BookshelfActions.less";
import AddModifyBookForm from "../AddModifyBook/AddModifyBook";

const BookshelfActions = () => {
    const [showAddModal, setShowAddModal] = useState(false);

    const handleCloseModal = () => setShowAddModal(false);
    const handleAddToCollectionClick = () => setShowAddModal(true);


  const dispatch = useAppDispatch();

  const handleRefresh = () => {
    dispatch(getBooksList());
  };

  return (
    <div className="bookshelfButtonActions">
          <Button onClick={handleAddToCollectionClick}><i className="bi bi-plus-circle"></i> Add To Collection</Button>
          <Button onClick={handleRefresh}><i className="bi bi-arrow-clockwise"></i> Refresh Books</Button>

          <Modal show={showAddModal} onHide={handleCloseModal}>
              <Modal.Header closeButton>
                  <Modal.Title>Add New Book to Collection</Modal.Title>
              </Modal.Header>
              <Modal.Body>
                  <AddModifyBookForm onClose={handleCloseModal} />
              </Modal.Body>
          </Modal>
    </div>
  );
};

export default BookshelfActions;
