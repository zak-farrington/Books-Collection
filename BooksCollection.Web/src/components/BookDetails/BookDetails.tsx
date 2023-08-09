import React from "react";
import { Button } from "react-bootstrap";
import { Book } from "../../models/Book";
import "./BookDetails.less";

const BookDetails: React.FC<{ book: Book }> = ({ book }) => {

    return (
        <div>
            <div>You can view, modify details or delete from collection.</div>
            <div className="bookDetailsButtonContainer">
                <Button><i className="bi bi-pencil-square"></i>Edit Details</Button>
                <Button variant="danger"><i className="bi bi-trash"></i> Delete</Button>
            </div>
        </div>
    )
}

export default BookDetails;
