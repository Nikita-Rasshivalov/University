import numpy as np
import cv2
import random
def activation(x):
    items = []
    for item in x:
        if item >= 0:
            items.append(1)
        else:
            items.append(-1)
    return np.array(items)
def to_binary_matrix(img):
    bin_img = []
    for iheight in range(img.shape[0]):
        for iwidth in range(img.shape[1]):
            val = np.average(img[iheight][iwidth])
            if val == 0:
                val = 1
            else:
                val = -1
            bin_img.append(val)
    return np.array(bin_img)

class SampleImage:
    def __init__(self, letter = '', noise = 0, image = []):
        self.letter = letter
        self.noise = noise
        self.image = image



class HopfieldNetwork:

    def __init__(self, train_samples) -> None:
        super().__init__()
        self.count_neurons = train_samples[0].shape[0]
        self.train_samples = train_samples
        self.weights = np.zeros(shape=(self.count_neurons, self.count_neurons))

    def train(self):
        for sample in self.train_samples:
            self.weights += sample * sample.reshape(len(sample), 1)
        self.weights /= self.count_neurons
        self.weights *= 1 - np.identity(self.count_neurons)

    def fit(self, sample):
        result = activation(self.weights.dot(sample.reshape(len(sample), 1)) / 2)  # f(W*X * 1/2)
        return result.flatten()



def add_noise(image, amount):
    indexes = []
    for i in range(len(image)):
        indexes.append(i)
    noise_count = int(amount*len(image))
    for i in range(noise_count):
        next = random.choice(indexes)
        indexes.remove(next)
        if image[next] == 1:
            image[next] = -1
        else:
            image[next] = 1    
    return image
def str_array_img(array_img):
    _str = '|'
    for i in range(array_img.shape[0]):
        if array_img[i] <= 0:
            _str += ' '
        else:
            _str += '█'
        if (i + 1) % 10 == 0:
            _str += '|\n|'
    return _str


if __name__ == '__main__':
    base_path = "D:\\University\\6sem\\SAPR\\lab9\\letters\\"
    paths = ["R.png","E.png","M.png"]
    sampleImages = []
    for path in paths:
        letter = path.split('.')[0]
        image = cv2.imread(base_path+path)
        sampleImages.append(SampleImage(letter,0*100, add_noise(to_binary_matrix(image), 0)))
        for i in range(1, 11):
            for j in range(10):
                sampleImages.append(SampleImage(letter, i/10.0*100, add_noise(to_binary_matrix(image), i/10.0)))
    print("All images")
    for img in sampleImages:
        print("----------------------------")
        print(str_array_img(img.image))
    np.random.shuffle(sampleImages)
    training, test = sampleImages[:int(0.8*len(sampleImages))], sampleImages[int(0.8*len(sampleImages)):]
    trainSamples = []
    for img in training:
        trainSamples.append(img.image)
    hn = HopfieldNetwork(trainSamples)
    hn.train()
    for image in test:
        result = hn.fit(image.image)
        print(f"{image.letter} {image.noise}%")
        print("before----------------------------")
        print(str_array_img(image.image))
        print("after----------------------------")
        print(str_array_img(result))