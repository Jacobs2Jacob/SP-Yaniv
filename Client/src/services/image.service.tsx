import { AxiosResponse } from 'axios';
import api from './api';
import { Image } from '../entities/image';
import { UploadImageRequest } from '../interfaces';

export const uploadImage = async (request: UploadImageRequest): Promise<AxiosResponse<number>> => {

    if (!request) {
        return null!;
    }

    const formData = new FormData();
    formData.append('file', request.file);
    formData.append('pictureName', request.pictureName);
    formData.append('pictureDate', request.pictureDate?.toString()!);
    formData.append('description', request.description!);

    return await api.post<number>(`/userAlbum/pictures/?userId=`, formData);
}

export const getImages = async (
    // @ts-ignore
    userId?: string
): Promise<AxiosResponse<Image[]>> => { 
    return await api.get<Image[]>(`/userAlbum/pictures/?userId=`);
}