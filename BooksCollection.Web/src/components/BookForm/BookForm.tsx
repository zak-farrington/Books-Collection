import React from 'react';
import { Form } from 'react-bootstrap';
import { Book, BookCategory } from '../../models/Book';
import { bookThumbnailUnavailableSrc } from '../../utils/constants';
import { formatCurrency } from '../../utils/formatters';
import "./BookForm.less";

const BookForm: React.FC<{
    book: Book | undefined;
    isReadOnly: boolean;
    onInputChange: (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => void;
    onCategoryChange: (value: BookCategory) => void;
}> = ({ book, isReadOnly, onInputChange, onCategoryChange }) => {

    const handleThumbnailClick = () => {
        const bookUrl = book?.imageUrl as string;
        if ((bookUrl?.length || 0) > 0) {
            window.open(bookUrl, "_blank");
        }
    }

    return (
        <div>
            <Form.Group className="formGroup">
                <Form.Label>Title</Form.Label>
                {isReadOnly ? <div>{book?.title || ""}</div> : <Form.Control type="text" name="title" value={book?.title || ""} onChange={onInputChange} />}
            </Form.Group>

            <Form.Group className="formGroup">
                <Form.Label>Description</Form.Label>
                {isReadOnly ? <div className="bookDescriptionLabel">{book?.description || ""}</div> : <Form.Control as="textarea" rows={3} name="description" value={book?.description || ""} onChange={onInputChange} />}
            </Form.Group>

            <Form.Group className="formGroup">
                <Form.Label>Author Name</Form.Label>
                {isReadOnly ? <div>{book?.authorName || ""}</div> : <Form.Control type="text" name="authorName" value={book?.authorName || ""} onChange={onInputChange} />}
            </Form.Group>

            {/*<Form.Group>*/}
            {/*    <Form.Label>Published Date</Form.Label>*/}
            {/*    <Form.Control type="date" name="publishedDate" value={formatDate()} onChange={handleInputChange} />*/}
            {/*</Form.Group>*/}


            <Form.Group className="formGroup">
                <Form.Label>MSRP</Form.Label>
                {isReadOnly ? <div>{book?.msrp ? formatCurrency(book.msrp) : "N/A"}</div> : <Form.Control type="number" name="msrp" value={book?.msrp || ""} onChange={onInputChange} />}
            </Form.Group>

            {/*<Form.Group className="formGroup">*/}
            {/*    <Form.Label>ISBN</Form.Label>*/}
            {/*    {isReadOnly ? (*/}
            {/*        <div>{book?.isbn || ""}</div>*/}
            {/*    ) : (*/}
            {/*        <Form.Control type="text" name="isbn" value={book?.isbn || ""} onChange={onInputChange} />*/}
            {/*    )}*/}
            {/*</Form.Group>*/}

            <Form.Group className="formGroup">
                <Form.Label>Category</Form.Label>
                {isReadOnly ? (
                    <div>{book?.category || ""}</div>
                ) : (
                    <Form.Control as="select" name="category" value={book?.category || ""} onChange={(e) => onCategoryChange(e.target.value as unknown as BookCategory)}>
                        {Object.values(BookCategory).map((category, index) => (
                            <option value={category} key={index}>{category}</option>
                        ))}
                    </Form.Control>
                )}
            </Form.Group>

            {book?.category === BookCategory.Other && (
                <Form.Group className="formGroup">
                    <Form.Label>Other Category Name</Form.Label>
                    {isReadOnly ? <div>{book?.otherCategoryName || ""}</div> : <Form.Control type="text" name="otherCategoryName" value={book?.otherCategoryName || ""} onChange={onInputChange} />}
                </Form.Group>
            )}

            <Form.Group className="formGroup">
                {isReadOnly ? (
                    <div>
                        <Form.Label>Image</Form.Label>
                        <img
                            className="bookDetailsThumbnail"
                            style={isReadOnly ? { cursor: "pointer"} : undefined}
                            onClick={handleThumbnailClick}
                            src={book?.imageUrl || bookThumbnailUnavailableSrc}
                            alt="Book Thumbnail" />
                    </div>
                ) : (
                    <div>
                        <Form.Label>Image URL</Form.Label>
                        <Form.Control type="text" name="imageUrl" value={book?.imageUrl || ""} onChange={onInputChange} />
                    </div>
                )}
            </Form.Group>
        </div>
    );
}

export default BookForm;
