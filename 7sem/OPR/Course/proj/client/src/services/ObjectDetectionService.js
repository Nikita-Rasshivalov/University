export class ObjectDetectionService {
    objectDetectionApiHost = "http://127.0.0.1:5010/";

    async processImage(image, threshold = 0.95) {
        try {
            var data = new FormData()
            data.append('file', image)
            let response = await fetch(this.objectDetectionApiHost + `/v1/object-detection/keras/image?threshold=${threshold}`, {
                method: "POST",
                body: data
            })
            let imageBlob = await response.blob();
            return await this.blobToBase64(imageBlob);
        } catch {
        }
        return null;
    }

    blobToBase64(blob) {
        return new Promise((resolve, _) => {
          const reader = new FileReader();
          reader.onloadend = () => resolve(reader.result);
          reader.readAsDataURL(blob);
        });
      }
}