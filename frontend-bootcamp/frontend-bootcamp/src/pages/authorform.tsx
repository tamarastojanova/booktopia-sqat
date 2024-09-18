import { useState, useEffect } from 'react';
import {
    useParams
  } from "react-router-dom";
import '../assets/authors.css'
import { useNavigate } from "react-router-dom";
import fetcher from '../Fetcher';
import '../assets/form.css'

function authorform() {
    const [author, setAuthor] = useState<Author>(() => {return {id: 0, firstName: '', lastName: '', imageUrl: ''}})
    let { id } = useParams<string>();
    const navigate = useNavigate();

    interface Author {
        id : number
        firstName : string
        lastName : string
        imageUrl : string
    }

    useEffect(() => {
        fetchInfo()
    },[])

    function fetchInfo() {
        if(id === undefined) {
            setAuthor({
                id: 0,
                firstName: '',
                lastName: '',
                imageUrl: ''
            })
        }
        else{
            fetcher<Author>(`https://localhost:44371/authors/${id}`)
            .then(data => setAuthor(data))
        }
    }

    const handleChange = (event : any) => {
        const name = event.target.name;
        const value = event.target.value;
        setAuthor(values => ({...values, [name]: value}))
      }

    function handleSubmit() {
        if(id === undefined) {
            fetcher<Author>(`https://localhost:44371/authors`, 'POST', author)
        }
        else {
            fetcher<Author>(`https://localhost:44371/authors/${id}`, 'PUT', author)
        }
        navigate('/authors')
    }
    
  return (
    <div className='form'>
      <div className='form-title'>Author Form</div>
    <div className='form-subtitle'>Enter the info for the Author</div>
      <div>
    <div className='input-container ic1'>
    <input className='input' type="text" name="firstName" value={author?.firstName}
            onChange={handleChange}/>
            <div className='cut'></div>
            <label className='placeholder'>First Name</label>
    </div>
    <div className='input-container ic2'>
    <input className='input' type="text" name="lastName" value={author?.lastName}
            onChange={handleChange}/>
            <div className='cut'></div>
            <label className='placeholder'>Last Name</label>
    </div>
    <div className='input-container ic1'>
    <input className='input' type="text" name="imageUrl" value={author?.imageUrl}
            onChange={handleChange}/>
            <div className='cut'></div>
            <label className='placeholder'>Image Url</label>
    </div>
        <button className='form-submit' onClick={() => handleSubmit()}>Submit</button>
    </div>
    </div>
  );
}
export default authorform;