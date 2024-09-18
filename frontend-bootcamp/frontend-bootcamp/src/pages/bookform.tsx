import { useState, useEffect } from 'react';
import {
    useParams
  } from "react-router-dom";
import '../assets/authors.css'
import { useNavigate } from "react-router-dom";
import fetcher from '../Fetcher';
import '../assets/form.css'

function bookForm() {
    const [book, setBook] = useState<Book>(() => {return {id: 0, title: '', yearPublished: 0, authorId: 0}})
    let { authorId } = useParams<string>();
    let { id } = useParams<string>();
    const navigate = useNavigate();

    interface Book{
        id : number
        title : string
        yearPublished : number
        authorId : number
    }

    useEffect(() => {
        fetchInfo()
    },[])

    function fetchInfo() {
        if(id === undefined) {
            setBook({
                id: 0,
                title: '',
                yearPublished: 0,
                authorId: parseInt(authorId!)
            })
        }
        else{
        fetcher<Book>(`https://localhost:44371/authors/${authorId}/books/${id}`)
        .then(data => setBook(data))
        }
    }

    const handleChange = (event : any) => {
        const name = event.target.name;
        const value = event.target.value;
        setBook(values => ({...values, [name]: value}))
      }

    function handleSubmit() {
        if(id === undefined) {
            fetcher<Book>(`https://localhost:44371/authors/${authorId}/books`, 'POST', book)
        }
        else {
            fetcher<Book>(`https://localhost:44371/authors/${authorId}/books/${id}`, 'PUT', book)
        }
        navigate(`/authors/${authorId}`)
    }
    
  return (
        <div className='form'>
      <div className='form-title'>Book Form</div>
    <div className='form-subtitle'>Enter the info for the Book</div>
      <div>
    <div className='input-container ic1'>
    <input className='input' type="text" name="title" value={book?.title}
            onChange={handleChange}/>
            <div className='cut'></div>
            <label className='placeholder'>Title</label>
    </div>
    <div className='input-container ic2'>
    <input className='input' type="text" name="yearPublished" value={book?.yearPublished}
            onChange={handleChange}/>
            <div className='cut'></div>
            <label className='placeholder'>Year Published</label>
    </div>
        <button className='form-submit' onClick={() => handleSubmit()}>Submit</button>
    </div>
    </div>
  );
}
export default bookForm;