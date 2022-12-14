import React, { useState } from 'react';
import { ObjectDetectionService } from '../../services/ObjectDetectionService';


export function ObjectDetection() {
    const [threshold, setTreshold] = useState(0.9);
    function handleTresholdChange(e){
        if(/^[0-9]\d*\.?\d*$/.test(e.target.value)){
        setTreshold(e.target.value);
        }
    }
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
       service.processImage(image, threshold).then((newImageSrc) => {
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
                <label>Treshold, how strict should be detection, use 0 to see all detections</label>
                <input onChange={handleTresholdChange} value={threshold} pattern="^[0-9]\d*\.?\d*$" type="text" className="form-input" maxLength="8"  placeholder="Treshold.." />

                <label>Image</label>
                <input onChange={handleImageChange} accept="image/png, image/jpeg" type="file" className="form-input" />
                <button className="form-submit" onClick={handleSubmit}>Show result</button>
            </div>
            <div className={imagesBlockClass}>
                <div className="grid-item">
                    <img src={imageSrc} width="245" height="356" />
                </div>
                <div className="grid-item">
                    <a download={currentFileName} className="details-button" href={imageSrc}>Download image</a>
                </div>
            </div>
            </div>

        </>
    );
}
