import React from "react";
import { Book } from "../../models/Book";
import BookThumbnail from "../BookThumbnail/BookThumbnail";
import "./BookshelfRow.less";


const BookshelfRow: React.FC<{ booksForRow: Book[] }> = ({ booksForRow }) => {
    // rest of the code

    return (
        <div className="bookshelfRow">
            {booksForRow.map((b, idx) => (
                <div key={`book-thumb-${idx}`} className="bookThumbnailContainer">
                    <BookThumbnail book={b} />
                </div>))
            }
        </div>
    );
}

export default BookshelfRow;
