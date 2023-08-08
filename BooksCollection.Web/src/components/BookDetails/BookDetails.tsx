import React from "react";
import { Button } from "react-bootstrap";
import "./BookDetails.less";

const BookDetails = () => (
  <div>
    <div>You can view, modify details or delete from collection.</div>
    <div className="bookDetailsButtonContainer">
      <Button><i className="bi bi-pencil-square"></i>Edit Details</Button>
      <Button variant="danger"><i className="bi bi-trash"></i> Delete</Button>
    </div>
    {/* Details and edit button here */}
  </div>
);

export default BookDetails;
