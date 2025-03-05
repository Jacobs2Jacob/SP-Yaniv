import { useEffect, useRef, useState } from 'react';
import { cloneDeep } from "lodash";
import styles from './App.module.css';
import { getImages, uploadImage } from '../../services/image.service';
import { Image } from '../../entities/image';
import { UploadImageRequest } from '../../interfaces'; 
import Modal from '../Modal/Modal';
 
const App = () => {
    const [images, setImages] = useState<Image[]>([]);
    const [formData, setFormData] = useState<UploadImageRequest>({
        file: null,
        pictureName: '',
        description: '',
        pictureDate: ''
    });
    const [errors, setErrors] = useState<any>({});
    const [submittionError, setSubmittionError] = useState<string>();
    const fileRef = useRef<HTMLInputElement>(null);
    const [modalVisible, setModalVisible] = useState(false);
     
    const handleChange = (e: any) => {
        const { name, value } = e.target;

        setFormData((prev) => {
            const cloned: any = cloneDeep(prev);
            cloned[name] = value;
            return cloned;
        })
    }

    const handleUploadClick = (e: any) => { 
        if (e.target.files[0]) {
            setFormData({ ...formData, file: e.target.files[0] });
        }
    };

    const handleSubmit = async (e: any) => {
        e.preventDefault();

        if (validate()) {
            const res = await uploadImage(formData);

            if (res.status === 200) {
                setImages((prev) => {
                    const cloned = cloneDeep(prev);
                    cloned.push({ id: res.data, name: formData.pictureName });
                    return cloned;
                });
            }
            else {  
                setSubmittionError(res.statusText);
            }
        }
    };

    const resetForm = () => {
        setFormData({ file: null, pictureName: '', description: '', pictureDate: '' });
        setSubmittionError('');

        if (fileRef?.current) {
            fileRef.current.value = null!;
        }
    }

    const validate = () => {
        let formErrors: any = {};

        if (!formData.file) {
            formErrors.file = 'Picture is required';
        }

        if (!formData.pictureName) {
            formErrors.pictureName = 'Picture Name is required';
        }

        setErrors(formErrors);
        return Object.keys(formErrors).length === 0;
    };
     
    // fetch data
    useEffect(() => {
        getImages().then(res => { 
            if (res.status === 200) {
                setImages(res.data);
            }
        });
    }, []);
     
    return (
        <div className='container'>

            {/* File List */}
            <div className={`${styles.fileList} col-4`}>
                <label style={{ margin: '0 auto' }}>File List</label>
                {images.map((m) => (
                    <div key={m.id} className={styles.fileListItem}>
                        <span className='col-4'>Id: {m.id}</span>
                        <span className='col-4'>Name: {m.name}</span>
                    </div>
                ))}
            </div>

            {/* Form */}
            <form className="form-container col-4" onSubmit={handleSubmit}>

                {/* File Input */}
                <div className="form-group">
                    <button className={styles.submitButton} type='button' onClick={() => fileRef.current?.click()}>Picture Browser</button>
                    <input
                        hidden
                        ref={fileRef}
                        accept=".jpg,.jpeg,.png"
                        type="file"
                        id="file" 
                        onChange={handleUploadClick}
                    />
                    {errors.file && <p className="error-text">{errors.file}</p>}
                </div>

                {/* Picture Name */}
                <div className="form-group">
                    <label htmlFor="pictureName">Picture Name: </label>
                    <input
                        type="text"
                        id="pictureName"
                        name="pictureName"
                        maxLength={50}
                        value={formData.pictureName}
                        onChange={handleChange}
                    />
                    {errors.pictureName && <p className="error-text">{errors.pictureName}</p>}
                </div>

                {/* Date */}
                <div className="form-group">
                    <label htmlFor="pictureDate">Picture Date: </label>
                    <input
                        type="date"
                        id="pictureDate"
                        name="pictureDate"
                        value={formData.pictureDate}
                        onChange={handleChange}
                    />
                </div>

                {/* File Name */}
                <div className="form-group">
                    <label>Picture File: </label>
                    <label>{formData?.file?.name}</label>
                </div>

                <div className="form-group">
                    <label htmlFor="description">Description: </label>
                    <textarea
                        id="description"
                        name="description"
                        maxLength={250}
                        value={formData.description}
                        onChange={handleChange}
                    />
                </div>

                {submittionError && <p className="error-text">{submittionError}</p>}

                {/* Buttons */}
                <div className="container">
                    <button type="submit" className={`${styles.submitButton} col-4`}>Add Picture</button>
                    <button className={`${styles.submitButton} col-4`} type='reset' onClick={() => setModalVisible(true)}>Reset</button>
                </div>
            </form> 

            <Modal
                cancelBtnText={'Cancel'}
                saveBtnText={'Reset'}
                show={modalVisible}
                text="Are you sure you want to reset the Form?"
                onSave={() => {
                    resetForm();
                    setModalVisible(false);
                }}
                onCancel={()=> setModalVisible(false)}
            />

        </div>
    )
}

export default App;