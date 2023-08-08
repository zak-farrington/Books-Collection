import React, { useState } from "react";
import "./BookThumbnail.less";
import { Modal, Button } from "react-bootstrap";
import BookDetails from "../BookDetails/BookDetails";

const BookThumbnail = () => {
  const [show, setShow] = useState(false);
  
  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);
  
  return (
  <div>
    <div className="thumbnail" onClick={handleShow}>
      Book Placeholder
    </div>
    <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Book Details</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <BookDetails/>
        </Modal.Body>
        {/* <Modal.Footer>
          <Button variant="secondary" onClick={handleClose}>
            Close
          </Button>
          <Button variant="primary" onClick={handleClose}>
            Save Changes
          </Button>
        </Modal.Footer> */}
      </Modal>
  </div>
  );
}

export default BookThumbnail;
