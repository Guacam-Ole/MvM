import { TestBed } from '@angular/core/testing';

import { PodcastUploadService } from './podcast-upload.service';

describe('PodcastUploadService', () => {
  let service: PodcastUploadService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PodcastUploadService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
