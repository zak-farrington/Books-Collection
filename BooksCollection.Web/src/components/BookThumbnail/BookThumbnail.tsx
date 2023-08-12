import React, { useState } from "react";
import { Modal, Button } from "react-bootstrap";
import "./BookThumbnail.less";
import BookDetails from "../BookDetails/BookDetails";
import { Book } from "../../models/Book";
import { bookThumbnailUnavailableSrc } from "../../utils/constants";

const BookThumbnail: React.FC<{ book: Book }> = ({ book }) => {
    const [showModal, setShowModal] = useState(false);

    const handleClose = () => setShowModal(false);
    const handleShow = () => setShowModal(true);

    const srcImageUrl = book?.imageUrl ?? bookThumbnailUnavailableSrc;

    return (
        <div>
            <div className="thumbnail" onClick={handleShow}>
                {srcImageUrl ? <img src={srcImageUrl} alt={book.title} title={book.title} className="img-thumbnail" /> : null}
            </div>
            <Modal show={showModal} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Book Details</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <BookDetails book={book} onClose={handleClose} />
                </Modal.Body>
            </Modal>
        </div>
    );
}

export default BookThumbnail;
