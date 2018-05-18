 export interface TestData
    {
        Id: number;
        Value: string;
    }


    export interface AuthResponse
    {
        id: string;

        auth_token: string;

        expires_in: number;
    }