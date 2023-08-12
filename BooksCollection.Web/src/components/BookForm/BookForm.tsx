import React from 'react';
import { Form } from 'react-bootstrap';
import { Book, BookCategory } from '../../models/Book';

const BookForm: React.FC<{
    book: Book | undefined;
    isReadOnly: boolean;
    onInputChange: (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => void;
    onCategoryChange: (value: BookCategory) => void;
}> = ({ book, isReadOnly, onInputChange, onCategoryChange }) => (
    <div>
        <Form.Group>
            <Form.Label>Title</Form.Label>
            {isReadOnly ? <div>{book?.title || ""}</div> : <Form.Control type="text" name="title" value={book?.title || ""} onChange={onInputChange} />}
        </Form.Group>

        <Form.Group>
            <Form.Label>Description</Form.Label>
            {isReadOnly ? <div>{book?.description || ""}</div> : <Form.Control as="textarea" rows={3} name="description" value={book?.description || ""} onChange={onInputChange} />}
        </Form.Group>

        <Form.Group>
            <Form.Label>Author Name</Form.Label>
            {isReadOnly ? <div>{book?.authorName || ""}</div> : <Form.Control type="text" name="authorName" value={book?.authorName || ""} onChange={onInputChange} />}
        </Form.Group>

        {/*<Form.Group>*/}
        {/*    <Form.Label>Published Date</Form.Label>*/}
        {/*    <Form.Control type="date" name="publishedDate" value={formatDate()} onChange={handleInputChange} />*/}
        {/*</Form.Group>*/}


        <Form.Group>
            <Form.Label>MSRP</Form.Label>
            {isReadOnly ? <div>{book?.msrp || ""}</div> : <Form.Control type="number" name="msrp" value={book?.msrp || ""} onChange={onInputChange} />}
        </Form.Group>

        {/*<Form.Group>*/}
        {/*    <Form.Label>ISBN</Form.Label>*/}
        {/*    <Form.Control type="text" name="isbn" value={localBook?.isbn || ""} onChange={handleInputChange} />*/}
        {/*</Form.Group>*/}

        <Form.Group>
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
            <Form.Group>
                <Form.Label>Other Category Name</Form.Label>
                {isReadOnly ? <div>{book?.otherCategoryName || ""}</div> : <Form.Control type="text" name="otherCategoryName" value={book?.otherCategoryName || ""} onChange={onInputChange} />}
            </Form.Group>
        )}

        <Form.Group>
            <Form.Label>Image URL</Form.Label>
            {isReadOnly ? <div>{book?.imageUrl || ""}</div> : <Form.Control type="text" name="imageUrl" value={book?.imageUrl || ""} onChange={onInputChange} />}
        </Form.Group>
    </div>
);

export default BookForm;
