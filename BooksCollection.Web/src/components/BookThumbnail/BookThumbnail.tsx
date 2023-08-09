import React, { useState } from "react";
import { Modal, Button } from "react-bootstrap";
import "./BookThumbnail.less";
import BookDetails from "../BookDetails/BookDetails";
import { Book } from "../../models/Book";

const BookThumbnail: React.FC<{ book: Book }> = ({ book }) => {
    const [show, setShow] = useState(false);

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    const srcImageUrl = book?.imageUrl as string;

    return (
        <div>
            <div className="thumbnail" onClick={handleShow}>
                {srcImageUrl ? <img src={book.imageUrl as string} alt={book.title} title={book.title} className="img-thumbnail" /> : null}
            </div>
            <Modal show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Book Details</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <BookDetails book={book} />
                </Modal.Body>
            </Modal>
        </div>
    );
}

export default BookThumbnail;
