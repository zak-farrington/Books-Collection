import React from "react";
import { Book } from "../../models/Book";
import BookThumbnail from "../BookThumbnail/BookThumbnail";
import "./BookshelfRow.less";


const BookshelfRow: React.FC<{ booksForRow: Book[] }> = ({ booksForRow }) => {
    // rest of the code

    return (
        <div className="bookshelfRow">
            {booksForRow.map(b => (
                <div className="bookThumbnailContainer">
                    <BookThumbnail book={b} />
                </div>))
            }
        </div>
    );
}

export default BookshelfRow;
