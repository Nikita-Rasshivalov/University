import React, { useState } from 'react';
import { ObjectDetectionService } from '../../services/ObjectDetectionService';


export function ObjectDetection() {
   
    const [imagesBlockClass, setImagesBlockClass] = useState("images-block");
    const [currentFileName, setCurrentFileName] = useState("result.png");
    const [imageSrc, setImageSrc] = useState(null);
    const [image, setImage] = useState(null);
    function handleImageChange(e){
        if(e.target.files.length === 0){
            return;
        }

        if(e.target.files.length > 1){
            alert("Cannot attach more then one file")
        }

        let file = e.target.files[0];
        if (file) {
            setCurrentFileName("result_" + file.name)
            setImage(file);
        }
    }

    function handleSubmit(){
       let service = new ObjectDetectionService();
       service.processImage(image).then((newImageSrc) => {
        if(newImageSrc !== null) 
        {
            setImageSrc(newImageSrc);
            setImagesBlockClass(imagesBlockClass + " " + "images-block-active");
        }
    });
    }

    return (
        <>
            <div className="all-container">
            <div className="form">
            

                <label>Изображение</label>
                <input onChange={handleImageChange} accept="image/png, image/jpeg" type="file" className="form-input" />
                <button className="form-submit" onClick={handleSubmit}>Обнаружить</button>
            </div>
            <div className={imagesBlockClass}>
                <div className="grid-item">
                    <img src={imageSrc} width="245" height="356" />
                </div>
                <div className="grid-item">
                    <a download={currentFileName} className="details-button" href={imageSrc}>Скачать изображение</a>
                </div>
            </div>
            </div>

        </>
    );
}
